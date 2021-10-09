import Category from "./category";
import { Profile } from "./profile";

export interface Topic{
    id: string;
    title: string;
    createdAt: Date | null;
    body: string;
    likes: number;
    category: Category;
    creator: Profile
}

export class TopicFormValues{
    id!: string;
    title!: string ;
    createdAt!: Date | null;
    body!: string;
    likes!: number;
    category!: Category;


    constructor(topic?: Topic){
        if(topic){
           this.id = topic.id;
           this.title = topic.title;
           this.body = topic.body;
           this.likes = 0;
           this.category = topic.category;
        }
    }
}
