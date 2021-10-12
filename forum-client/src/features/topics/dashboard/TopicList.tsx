import { observer } from "mobx-react-lite";
import React, { Fragment, useEffect } from "react";
import { Header } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";
import TopicListItem from "./ListItem";

export default observer( function TopicList(){

    const {topicStore} = useStore();
    const{topicsByDate} = topicStore;


    
    return(
        <>
        
        {topicsByDate.map(topic => (
             <TopicListItem key={topic.id} topic = {topic} />
         ))}
        </>
    ); 
}
)