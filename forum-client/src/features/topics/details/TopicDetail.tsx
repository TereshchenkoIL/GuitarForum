import { observer } from "mobx-react-lite";
import React, { useEffect } from "react";
import { useParams } from "react-router-dom";
import LoadingComponent from "../../../app/layout/LoadingComponent";
import { useStore } from "../../../app/stores/store";
import TopicDetailChat from "./TopicDetailChat";
import TopicDetailHeader from "./TopicDetailHeader";

export default observer( function TopicDetail(){
    
    const{topicStore} = useStore();

    const {selectedTopic: topic, loadTopic, clearSelectedTopic } = topicStore;
    const { id } = useParams<{ id: string }>();

    useEffect(() => {
        if (id) loadTopic(id)
        return () => clearSelectedTopic();
    }, [id, loadTopic, clearSelectedTopic])
    
    if(!topic) return <LoadingComponent content='Loading...' />
    return(
        <>
            <TopicDetailHeader topic={topic!} />
            <TopicDetailChat topicId={topic!.id} />
        </>
    );
}
)