import React, { useEffect } from "react";
import { Button, Icon, Item, Label, Segment } from "semantic-ui-react";
import { Topic } from "../../../app/models/topic";
import { format } from 'date-fns'
import { Link } from "react-router-dom";
import { useStore } from "../../../app/stores/store";
import { observer } from "mobx-react-lite";

interface Props{
    topic: Topic;
}

export default observer( function TopicListItem({topic} : Props)
{
    const {topicStore, userStore:{isAdmin, user}} = useStore();
    useEffect(() => {
    })
    return (
        <Segment.Group>
            <Segment>
            
                <Item.Group>
                    <Item key={topic.id}>
                        <Item.Image style={{ marginBottom: 3 }} size='tiny' circular src={topic.creator?.image || '/assets/user.png'} />
                        <Item.Content>
                            <Item.Header as={Link} to={`/topics/${topic.id}`}>
                                {topic.title} {topic.category.name}
                            </Item.Header>
                            <Item.Description>Created by <Link to={`/profiles/${topic.creator.username}`}> {topic.creator.displayName}</Link></Item.Description>
                    
                        </Item.Content>
                    </Item>
                </Item.Group>
            </Segment>
            <Segment>
                <span>
                    <Icon name='clock' /> {format(topic.createdAt!, 'dd MMMM yyyy h:mm aa')}
                    <Icon name='heart' /> {topic.likes}
                </span>
            </Segment>
            
            <Segment clearing>

                {(topic.creator.username === user!.username || isAdmin) && (
                    <Button 
                    onClick={() => topicStore.deleteTopic(topic.id)}
                    color='red'
                    floated='right'
                    content='Delete' />
                )}
                

                <Button 
                    as={Link}
                    to={`/topics/${topic.id}`}
                    color='teal'
                    floated='right'
                    content='View' />
            </Segment>
        </Segment.Group>
    );
})