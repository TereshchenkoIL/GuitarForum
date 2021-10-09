import { createContext, useContext } from "react";
import CategoryStore from "./categoryStore";
import TopicStore from "./topicStore";

interface Store{
    topicStore: TopicStore,
    categoryStore: CategoryStore
}


export const store: Store = {
    topicStore: new TopicStore(),
    categoryStore: new CategoryStore()
}

export const StoreContext = createContext(store);

export function useStore(){
    return useContext(StoreContext);
}