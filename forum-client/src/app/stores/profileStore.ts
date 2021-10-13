import { makeAutoObservable, runInAction } from "mobx";
import agent from "../api/agent";
import { Profile, ProfileActivityValue, ProfileUpdateData } from "../models/profile";
import { Topic } from "../models/topic";
import { store } from "./store";

export default class ProfileStore{
    profile: Profile | null = null;
    loadingProfile = true;
    uploading = false;
    topics: Topic[] = []
    loading = true;
    activity: ProfileActivityValue[] = []
    constructor(){
        makeAutoObservable(this)
    }

    get isCurrentUser(){

        if(store.userStore.user && this.profile){
            return store.userStore.user.username === this.profile.username
        }
        return false;
    }
    loadProfile = async (username: string) => {
       this.loadingProfile = true;
        try{
            const profile = await agent.Profiles.get(username);
            console.log(profile)

            runInAction(() => {
                this.profile = profile
                this.loadingProfile = false;
            })
        } catch(error){
            console.log(error);
            runInAction(() => {
                this.loadingProfile = false;
            })
        }
    }

    updateProfile = async (updateData: ProfileUpdateData) => {
        this.loading = true;

        try{
            var profile = await agent.Profiles.updateProfile(updateData);
            var user = await agent.Account.current();
            runInAction(() => {
                if(profile){
                    this.profile = profile;
                    store.userStore.user = user;
                    this.loading = false;
                    
                }
            });
        } catch(error) {
            
            runInAction(() => this.loading=false );
            throw error;
        }
    }

    loadTopic = async (username: string) => {
        this.loading = true;

        try{
            const topics = await agent.Profiles.getTopics(username);

            runInAction(() => {
                this.topics = topics;
                this.loading = false;
            })

        }catch(error){
            console.log(error)
            runInAction(() => {
                this.loading = false;
            })
        }
    }
    
    deletePhoto = async(id: string) =>{
        try{
            await agent.Profiles.deletePhoto(id);

            runInAction(() =>{
               if(this.profile)
               {
                this.profile.image = undefined;
                if(this.profile.username === store.userStore.user!.username)
                {
                    store.userStore.setImage(undefined);
                }
               }
            })
        } catch(error){
            console.log(error)
        }
    }


    uploadPhoto = async (file: Blob) => {
        this.uploading = true;
        try{
            const response = await agent.Profiles.uploadPhoto(file);
            const photo = response.data;

            runInAction(() => {
                if(this.profile){
                    this.profile.photo = photo;
                    store.userStore.setImage(photo.url);
                    this.profile.image = photo.url;
   
                    this.uploading = false;
                    console.log("upload")
                
                }
            });
        } catch (error) {
            console.log(error);
            runInAction(() => this.uploading = false);
        }
    }

    loadActivity = async(username: string) => {
        this.loading = true;
        try{
            const activity = await agent.Profiles.getProfileActivity(username);
          console.log(activity)
            runInAction(() => {
                this.loading = false;
                this.activity = activity;
                
            })
        }catch(error){
            console.log(error)
            runInAction(() => {
                this.loading = false;
            })
        }
    }

}