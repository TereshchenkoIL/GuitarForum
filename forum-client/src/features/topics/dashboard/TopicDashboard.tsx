
import { observer } from "mobx-react-lite";
import React, { useEffect, useState } from "react";
import { Button, Grid, Loader } from "semantic-ui-react";
import LoadingComponent from "../../../app/layout/LoadingComponent";
import { PagingParams } from "../../../app/models/pagination";
import { useStore } from "../../../app/stores/store";
import CategoryMenu from "./CategoryMenu";
import TopicList from "./TopicList";

export default observer(function TopicDashboard() {

    const {topicStore} = useStore();
    const {loadAllTopics, loadingInitial} = topicStore;
    

    useEffect(() => {
        loadAllTopics();
    }, [loadAllTopics])
    return (
        <Grid>
            <Grid.Column width='10'>
                <CategoryMenu />
                {loadingInitial ? 
                
               <LoadingComponent content='Loading...'/> :
                <TopicList />}

            </Grid.Column>
        
        </Grid>
    );
});