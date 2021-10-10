import { createContext, useContext } from "react";
import CategoryStore from "./categoryStore";
import CommentStore from "./commentStore";
import ModalStore from "./modalStore";
import TopicStore from "./topicStore";
import UserStore from "./userStore";

interface Store{
    topicStore: TopicStore,
    categoryStore: CategoryStore,
    modalStore: ModalStore,
    userStore: UserStore,
    commentStore: CommentStore
}


export const store: Store = {
    topicStore: new TopicStore(),
    categoryStore: new CategoryStore(),
    modalStore: new ModalStore(),
    userStore: new UserStore(),
    commentStore: new CommentStore()
}

export const StoreContext = createContext(store);

export function useStore(){
    return useContext(StoreContext);
}