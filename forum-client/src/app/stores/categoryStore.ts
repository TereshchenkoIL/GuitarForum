import { makeAutoObservable } from "mobx";
import Category from "../models/category";


export default class CategoryStore{
    categories: Category[] = [];


    constructor(){
        makeAutoObservable(this);
    }
}