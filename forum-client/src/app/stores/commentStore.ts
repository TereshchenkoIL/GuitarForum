import { HubConnection, HubConnectionBuilder, LogLevel } from "@microsoft/signalr";
import { makeAutoObservable, runInAction } from "mobx";
import { ChatComment, CommentUpdateDto } from "../models/comment";
import { store } from "./store";

export default class CommentStore{
    comments = new Map<string, ChatComment>();
    editMode = false;
    selectedComment: ChatComment | null = null;
    hubConnection: HubConnection | null = null;

    constructor(){
        makeAutoObservable(this);
    }

    private setComment = (comment: ChatComment) => {
        this.comments.set(comment.id, comment);
    }
    get commentsByDate(){
        return Array.from(this.comments.values()).sort((a, b) => b.createdAt!.getTime() - a.createdAt!.getTime())
    }
    createHubConnection = (topicId: string) => {
        if(store.topicStore.selectedTopic){
            console.log( store.userStore.token!)
            this.hubConnection = new HubConnectionBuilder()
            .withUrl('http://localhost:5000/chat?topicId='+topicId,{
                accessTokenFactory: () => store.userStore.user?.token!
            })
            .withAutomaticReconnect()
            .configureLogging(LogLevel.Information)
            .build();

            this.hubConnection.start().catch(error => console.log("Error establishing the signalR connection: " + error));

            this.hubConnection.on('LoadComments', (comments: ChatComment[]) => {
              
                runInAction(() => {  
                   alert('LoadComments');
                    comments.forEach(comment => {
                        comment.createdAt = new Date(comment.createdAt);
                        this.setComment(comment);
                    })
                })
            })

            this.hubConnection.on('ReceiveComment', (comment: ChatComment) => {
              
                runInAction(() => {  
                   alert('Receive');
                    
                        comment.createdAt = new Date(comment.createdAt);
                        this.setComment(comment);
                   
                })
            })
            this.hubConnection.on('UpdateComment', (comment: CommentUpdateDto) => {
                
                runInAction(() => {
                    let commentFromRegistry = this.comments.get(comment.id)

                    if(commentFromRegistry){
                        this.comments.delete(comment.id);
                        commentFromRegistry.body = comment.body;
                        this.setComment(commentFromRegistry);
                    }
                })
            });

            this.hubConnection.on('DeleteComment', (id: string) => {
                runInAction(() => {
                   this.comments.delete(id);
                })
            });

            this.hubConnection.onclose((e) => alert("Error"))
        }
    }



    clearComments = () =>{
        this.comments = new Map<string, ChatComment>();
       // this.stopHubConnection();
    }

    addComent = async (values: any) => {
        values.topicId = store.topicStore.selectedTopic?.id;
        values.username = store.userStore.user?.username;

        try{
            await this.hubConnection?.invoke('SendComment', values);
        } catch(error) {
            console.log(error);
        }
    }
}