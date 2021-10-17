import { makeAutoObservable, reaction, runInAction } from "mobx";
import agent from "../api/agent";
import { User, UserFormValues } from "../models/user";
import { store } from "./store";
import {history} from '../..'

export default class UserStore{
    user: User | null = null;
    token: string | null = window.localStorage.getItem('jwt');
    refreshTokenTimeout: any;

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

    get isAdmin(){
        return this.user?.isAdmin;
    }
    setToken = (token: string|null) =>{
        if(token) window.localStorage.setItem('jwt', token);
        this.token = token;
    }

    login = async(creds: UserFormValues) => {
        try{
            const user = await agent.Account.login(creds);
            this.setToken(user.token)
            this.startRefershTokenTimer(user);
            runInAction(() => {
                this.user = user;
                console.log(user);
            })
            
            history.push('/topics')
        }catch(error){
            throw error;
        }
    }

    logout = () =>{
        this.setToken(null);
        window.localStorage.removeItem('jwt');
        this.user = null;   
        history.push('/')  
    }

    getUser = async () => {
        try{
            const user = await agent.Account.current();
            this.setToken(user.token)
            runInAction(() => this.user = user)
            this.startRefershTokenTimer(user);
        }catch (error){
            console.log(error);
        }
    }

    register = async (creds: UserFormValues) => {
        try{
            const user = await agent.Account.register(creds);
            this.setToken(user.token)
            this.startRefershTokenTimer(user);
            runInAction(() => {
                this.user = user;
            }) 
            
            history.push('/topics')
        }catch(error){
            console.log(error)
            throw error;
        }

      
    }

    setImage = (image: string | undefined) => {
       if(this.user) this.user.image = image;
    }

    refreshToken = async () => {
        this.stopRefreshTokenTimer();
        try{
            const user = await agent.Account.refreshToken();
            runInAction(() => {
                this.user = user;
                this.setToken(user.token);
            });

            this.startRefershTokenTimer(user);
            
        }catch(error){
            console.log(error);
        }
    }

    private startRefershTokenTimer(user: User){
        const jwtToken = JSON.parse(atob(user.token.split('.')[1]));

        const expires = new Date(jwtToken.exp * 1000);

        const timeout = expires.getTime() - Date.now() - (60 * 1000);

        this.refreshTokenTimeout = setTimeout(this.refreshToken, timeout);
    }

    private stopRefreshTokenTimer(){
        clearTimeout(this.refreshTokenTimeout);
    }
    
}