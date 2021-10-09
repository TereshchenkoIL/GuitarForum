import { makeAutoObservable, reaction, runInAction } from "mobx";
import agent from "../api/agent";
import { User, UserFormValues } from "../models/user";
import { store } from "./store";

export default class UserStore{
    user: User | null = null;
    token: string | null = window.localStorage.getItem('jwt');

    constructor() {
        makeAutoObservable(this);
        reaction(() => this.token,
        token =>{
            if(token){
                window.localStorage.setItem('jwt', token)
            } else {
                window.localStorage.removeItem('jwt')
            }
        })
    }

    get isLoggedIn(){
        return !!this.user;
    }
    setToken = (token: string|null) =>{
        if(token) window.localStorage.setItem('jwt', token);
        this.token = token;
    }

    login = async(creds: UserFormValues) => {
        try{
            const user = await agent.Account.login(creds);
            this.setToken(user.token)
            runInAction(() => {
                this.user = user;
                console.log(user);
            })
            
            store.modalStore.closeModal();
        }catch(error){
            throw error;
        }
    }

    logout = () =>{
        this.setToken(null);
        window.localStorage.removeItem('jwt');
        this.user = null;     
    }

    getUser = async () => {
        try{
            const user = await agent.Account.current();

            runInAction(() => this.user = user)
        }catch (error){
            console.log(error);
        }
    }

    register = async (creds: UserFormValues) => {
        try{
            const user = await agent.Account.register(creds);
            this.setToken(user.token)
            runInAction(() => {
                this.user = user;
            })
            store.modalStore.closeModal();
        }catch(error){
            throw error;
        }
    }

    setImage = (image: string) => {
       if(this.user) this.user.image = image;
    }
    
}