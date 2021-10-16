
import { observer } from "mobx-react-lite";
import React, { useEffect, useState } from "react";
import { Button, Grid, Loader } from "semantic-ui-react";
import LoadingComponent from "../../../app/layout/LoadingComponent";
import { PagingParams } from "../../../app/models/pagination";
import { useStore } from "../../../app/stores/store";
import CategoryMenu from "./CategoryMenu";
import TopicList from "./TopicList";
import InfiniteScroll from "react-infinite-scroller";
import sleep from "../../../app/common/Sleep";
export default observer(function TopicDashboard() {

    const {topicStore} = useStore();
    const {loadAllTopics, loadingInitial, pagination, setPagingParams, loadByCategory, loadTopicsByCaetgory, topicRegistry, category} = topicStore;
    const [loadingNext, setLoadingNext] = useState(false);
    
    async function handleGetNext(){
        setLoadingNext(true);
        setPagingParams(new PagingParams(pagination!.currentPage +1))
        await sleep(400);
        if(loadByCategory){

            loadTopicsByCaetgory().then(() => setLoadingNext(false))
        }else{
             loadAllTopics().then(() => setLoadingNext(false))
        }
        
    }

    useEffect(() => {
        loadAllTopics();
    }, [loadAllTopics])
    return (
        <Grid>
            <Grid.Column width='10'>
                <CategoryMenu />
                {loadingInitial ? 
                
               <LoadingComponent content='Loading...'/> :
               <InfiniteScroll
                    pageStart={0}
                    loadMore={handleGetNext}
                    hasMore={!loadingNext && !!pagination && pagination.currentPage < pagination.totalPages}
                    initialLoad={false}
                >
                    <TopicList />
                </InfiniteScroll>
               }

            </Grid.Column>
        
        <Grid.Column width={10}>
            <Loader active={loadingNext} />
        </Grid.Column>
        </Grid>
    );
});