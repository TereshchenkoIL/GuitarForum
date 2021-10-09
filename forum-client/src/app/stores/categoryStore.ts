import { makeAutoObservable } from "mobx";
import agent from "../api/agent";
import Category from "../models/category";


export default class CategoryStore{
    categories: Category[] = [];
    loadingInitial = true;


    constructor(){
        makeAutoObservable(this);
    }

    setLoadingInitial = (state: boolean) => {
        this.loadingInitial = state;
    }
    setCategories = (categories: Category[]) => {
        this.categories = categories;
    }
    loadCategories = async () => {
        try{
            this.setLoadingInitial(true);
            const result = await agent.Categories.list();
            this.setCategories(result);
            this.setLoadingInitial(false);
        }catch(exception){
            console.log(exception);
            this.setLoadingInitial(false);
        }
    }

    get Categories(){
        return this.categories;
    }
}