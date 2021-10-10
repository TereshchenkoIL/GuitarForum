export interface ChatComment{
    id: string;
    createdAt: Date;
    body: string;
    username: string;
    displayName: string;
    image: string;
    topicId: string;
}

export interface CommentUpdateDto{
    id: string;
    body: string;
}