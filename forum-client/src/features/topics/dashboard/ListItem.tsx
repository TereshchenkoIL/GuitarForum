import React, { useEffect } from "react";
import { Button, Icon, Item, Label, Segment } from "semantic-ui-react";
import { Topic } from "../../../app/models/topic";
import { format } from 'date-fns'

interface Props{
    topic: Topic;
}

export default function TopicListItem({topic} : Props)
{

    useEffect(() => {
     console.log("Item");
    })
    return (
        <Segment.Group>
            <Segment>
            
                <Item.Group>
                    <Item key={topic.id}>
                        <Item.Image style={{ marginBottom: 3 }} size='tiny' circular src={topic.creator?.image || '/assets/user.png'} />
                        <Item.Content>
                            <Item.Header >
                                {topic.title}
                            </Item.Header>
                    
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
                <Button 
                    color='red'
                    floated='right'
                    content='Delete' />
    
                <Button 
                    color='teal'
                    floated='right'
                    content='View' />
            </Segment>
        </Segment.Group>
    );
}