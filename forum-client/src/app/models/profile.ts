import { User } from "./user";

export interface Profile {
    username: string;
    displayName: string;
    image?:string;
    bio: string;
    photo: Photo;
    topicsCount: number;
}

export class Profile implements Profile{
    constructor(user: User){
        this.username = user.username;
        this.displayName = user.displayName;
        this.image = user.image;
    }
}
export interface Photo{
    id: string;
    url: string;

}
export interface ProfileUpdateData{
    displayName?: string;
    bio?: string;
}

export interface ProfileActivityValue{
    date: string,
    count: number
}