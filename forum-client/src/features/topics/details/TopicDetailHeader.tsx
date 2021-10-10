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

   
    useEffect(() => {
        
       console.log(topic)
    }, [topic])

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
                                <Button>  <Icon color='red' name='heart' /> {topic.likes} </Button>
                                :
                                <Button>  <Icon color='red' name='heart outline' /> {topic.likes} </Button>
                                }
                                
                            </Item.Extra>
                        </Item.Content>
                    </Item>
                </Item.Group>
            </Grid.Column>
        </Grid>
    )



})