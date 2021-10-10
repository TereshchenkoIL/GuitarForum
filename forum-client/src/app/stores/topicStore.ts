import { makeAutoObservable, reaction, runInAction } from "mobx";
import agent from "../api/agent";
import Category from "../models/category";
import { Pagination, PagingParams } from "../models/pagination";
import { Topic, TopicFormValues } from "../models/topic";

export default class TopicStore{
    topicRegistry = new Map<string, Topic>();
    selectedTopic: Topic | null = null;
    editMode = false;
    loading = false;
    loadingInitial = true;
    pagination: Pagination | null = null;
    pagingParams = new PagingParams();

    loadByCategory = false;
    category: Category | null = null;
    
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

        reaction(
            () => this.category,
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

    setCategory = (category: Category) => {
        this.category = category;
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
            const result = await agent.Topics.listByCategory(this.category!.id!,this.axiosParams);
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

    loadTopic = async (id: string) => {
        try{
            this.setLoadingInitial(true);
            const topic = await agent.Topics.details(id);
            
            topic.createdAt = new Date(topic.createdAt!);

            runInAction(() => {
                this.selectedTopic = topic;
            })
    
            this.setLoadingInitial(false);              
        }catch(error){
            console.log(error);
            this.setLoadingInitial(false);  
        }
    }
    clearSelectedTopic = () => {
        this.selectedTopic = null;
    }


    
    private getTopic = (id: string) =>{
        return this.topicRegistry.get(id);
    }

    updateTopic = async (topic: TopicFormValues) => {
        try{
            await agent.Topics.update(topic);

            runInAction(() => {
                if(topic.id){
                    let updatedtopic = {...this.getTopic(topic.id), ...topic}
                    this.topicRegistry.set(topic.id, updatedtopic as Topic);
                    this.selectedTopic = updatedtopic as Topic;
                }

            })

        }catch(error)
        {
            console.log(error);

            runInAction(() => {
                this.loading = false;
            })
        }
    }

    deleteTopic = async (id: string) => {
        this.loading = true;

        try{
            await agent.Topics.delete(id);

            runInAction(() => {
               this.topicRegistry.delete(id);
               this.loading = false
            })

        }catch(error){
            console.log(error);

            runInAction(() => {
                this.loading = false;
            })
        }
    }
}