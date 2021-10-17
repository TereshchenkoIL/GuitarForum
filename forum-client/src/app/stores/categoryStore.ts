import { makeAutoObservable, runInAction } from "mobx";
import agent from "../api/agent";
import Category, { CategoryValues } from "../models/category";
import { history } from "../..";
import { toast } from "react-toastify";

export default class CategoryStore{
    categories: Category[] = [];
    selectedCategory: Category | null = null;
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

    loadById = async (categoryId: string) => {
        try{
            this.setLoadingInitial(true);
            const category = await agent.Categories.detail(categoryId);

            runInAction(() => {
                this.selectedCategory = category;
                this.setLoadingInitial(false);
            })
        }catch(error){
            console.log(error);
            this.setLoadingInitial(false);
        }
    }
    
    deleteCategory = async (id: string) => {
        try{
            await agent.Categories.delete(id);

            runInAction(() => {
                this.categories = this.categories.filter((category) => category.id !== id)
            })

        }catch(error){
            console.log(error);
        }
    } 

    createCategory = async (category: CategoryValues) =>{
        try{
            await agent.Categories.create(category);

            runInAction(() =>{
                this.categories.push(category)
            })

            history.push('/categories')
        }catch(error){
          throw error;
        }
    }

    updateCategory = async (category: CategoryValues) =>{
        try{
            await agent.Categories.update(category);

            runInAction(() =>{
                this.categories = this.categories.filter(x => x.id !== category.id)
                this.categories.push(category)
            })
            history.push('/categories')
        }catch(error){
            console.log(error)
           throw error
        }
    }

    get Categories(){
        return this.categories;
    }
}