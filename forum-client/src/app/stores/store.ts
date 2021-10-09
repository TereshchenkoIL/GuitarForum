import { createContext, useContext } from "react";
import CategoryStore from "./categoryStore";
import ModalStore from "./modalStore";
import TopicStore from "./topicStore";
import UserStore from "./userStore";

interface Store{
    topicStore: TopicStore,
    categoryStore: CategoryStore,
    modalStore: ModalStore,
    userStore: UserStore
}


export const store: Store = {
    topicStore: new TopicStore(),
    categoryStore: new CategoryStore(),
    modalStore: new ModalStore(),
    userStore: new UserStore()
}

export const StoreContext = createContext(store);

export function useStore(){
    return useContext(StoreContext);
}