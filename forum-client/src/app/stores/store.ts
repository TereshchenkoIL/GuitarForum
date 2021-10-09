import { createContext, useContext } from "react";
import TopicStore from "./topicStore";

interface Store{
    topicStore: TopicStore
}


export const store: Store = {
    topicStore: new TopicStore()
}

export const StoreContext = createContext(store);

export function useStore(){
    return useContext(StoreContext);
}