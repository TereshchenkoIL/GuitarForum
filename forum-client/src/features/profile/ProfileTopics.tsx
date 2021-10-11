import { observer } from "mobx-react-lite";
import React, { useEffect } from "react";
import { Link } from "react-router-dom";
import { Card, Icon } from "semantic-ui-react";
import { Profile } from "../../app/models/profile";
import { useStore } from "../../app/stores/store";

interface Props{
    profile: Profile
}

export default observer(function Topics({profile}: Props){

    const{profileStore} = useStore();

    const{loadTopic,  topics} = profileStore;

    useEffect(() => {
        loadTopic(profile.username)
    }, [loadTopic, profile.username])

    return(
        <>
            {topics && 
            
            <Card.Group>
                {topics.map(topic => (
                    

                    <Card key={topic.id}>
                        <Card.Header><Link to={`/topics/${topic.id}`}><h4>{topic.title}</h4> </Link></Card.Header>
                        <Card.Description><p>{topic.body.substr(0, 30) + '...'}</p></Card.Description>
                        <Card.Content extra>
                            <div>
                                <Icon name='heart' />
                                {topic.likes}
                             </div>
                        </Card.Content>
                    </Card>
                ))}
           </Card.Group> 
           }
          
        </>
    )

})