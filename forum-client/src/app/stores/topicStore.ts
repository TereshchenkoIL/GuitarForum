import { makeAutoObservable, reaction, runInAction } from "mobx";
import agent from "../api/agent";
import { Pagination, PagingParams } from "../models/pagination";
import { Topic } from "../models/topic";

export default class TopicStore{
    topicRegistry = new Map<string, Topic>();
    selectedTopic: Topic | null = null;
    editMode = false;
    loading = false;
    loadingInitial = true;
    pagination: Pagination | null = null;
    pagingParams = new PagingParams();

    loadByCategory = false;
    categoryId: string | null = null;
    
    constructor(){
        makeAutoObservable(this);
        reaction(
            () => this.loadByCategory,
            () => {
                this.pagingParams = new PagingParams();
                this.topicRegistry.clear();
                if(this.loadByCategory){
                    this.loadTopicsByCaetgory()
                }else{
                    this.loadAllTopics();
                }
            }
        );
    }

    get axiosParams(){
        const params = new URLSearchParams();

        params.append('PageNumber', this.pagingParams.pageNumber.toString());
        params.append('PageSize', this.pagingParams.pageSize.toString());
        return params;
    }

    setPagingParams = (pagingParams: PagingParams) => {
        this.pagingParams = pagingParams;
    }

    setPagination = (pagination: Pagination) => {
        this.pagination = pagination;
    }
    setLoadingInitial = (state: boolean) => {
        this.loadingInitial = state;
    }

    setLoadByCategory = (state: boolean) => {
        this.loadByCategory = state;
    }

    setCategoryId = (id: string) => {
        this.categoryId = id;
    }
    get topicsByDate(){
        return Array.from(this.topicRegistry.values()).sort((a, b) => a.createdAt!.getTime() - b.createdAt!.getTime())
    }

    loadAllTopics = async () => {
        try{
            this.setLoadingInitial(true);
            this.loading = true;
            const result = await agent.Topics.list(this.axiosParams);
            result.data.forEach(topic =>{
                this.setTopic(topic);
                })    
          
            this.setLoadingInitial(false);              
            this.setPagination(result.pagination)
        }catch(error){
            console.log(error);
            this.setLoadingInitial(false);  
        }
    }


    loadTopicsByCaetgory = async () => {
        try{
            this.setLoadingInitial(true);
            this.loading = true;
            const result = await agent.Topics.listByCategory(this.categoryId!,this.axiosParams);
            result.data.forEach(topic =>{
                this.setTopic(topic);
                })    
          
            this.setLoadingInitial(false);              
            this.setPagination(result.pagination)
        }catch(error){
            console.log(error);
            this.setLoadingInitial(false);  
        }
    }

    private setTopic = (topic: Topic) => {

        topic.createdAt = new Date(topic.createdAt!);
        this.topicRegistry.set(topic.id, topic);
    }

}