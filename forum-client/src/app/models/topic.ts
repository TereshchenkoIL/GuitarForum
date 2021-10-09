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