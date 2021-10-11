import { observer } from "mobx-react-lite";
import React, { useEffect } from "react";
import { Link } from "react-router-dom";
import { Item, Image, Icon, Button, Container, Grid } from "semantic-ui-react";
import { Topic } from "../../../app/models/topic";
import { useStore } from "../../../app/stores/store";

interface Props{
    topic: Topic
}
export default observer(function TopicDetailHeader({topic} : Props){

   const{userStore, topicStore:{likeTopic, loading}} = useStore();
    const {user, isAdmin} = userStore
    useEffect(() => {
        
       userStore.getUser();

       console.log(userStore.user)
    }, [userStore])

    return(
        <Grid centered>
            <Grid.Column width={14}>
                <Item.Group>
                    <Item>
                        <Item.Image size='tiny'src={topic.creator?.image || '/assets/user.png'}/>

                        <Item.Content>
                            <Item.Header >{topic.title}</Item.Header>
                            <Item.Meta>Started by <Link to='/'>{topic.creator.displayName}</Link></Item.Meta>
                            <Item.Description>
                                <p>{topic.body}</p>
                            </Item.Description>

                            <Item.Extra>
                                {topic.isLiked ? 
                                <Button disabled={loading} onClick={() => likeTopic(topic.id)}>  <Icon color='red' name='heart' /> {topic.likes} </Button>
                                :
                                <Button disabled={loading} onClick={() => likeTopic(topic.id)}>  <Icon color='red' name='heart outline' /> {topic.likes} </Button>
                                }

                                { (topic.creator.username === userStore.user!.username || isAdmin)?
                                    <Button as={Link} to={`/editTopic/${topic.id}`}  content='Edit' color='orange'/>:
                                    user?.username
                                    
                                }
                                
                            </Item.Extra>
                        </Item.Content>
                    </Item>
                </Item.Group>
            </Grid.Column>
        </Grid>
    )



})