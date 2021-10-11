import { observer } from "mobx-react-lite";
import React, { useState } from "react";
import { Card, Header, Tab, Image, Grid, Button } from "semantic-ui-react";
import PhotoUploadWidget from "../../app/common/imageUpload/PhotoUploadWidget";
import { Profile } from "../../app/models/profile";
import { useStore } from "../../app/stores/store";

interface Props{
    profile: Profile
}

export default observer(function ProfilePhoto({profile}:Props){

    const {profileStore: {isCurrentUser}} = useStore();
    const [addPhotoMode, setAddPhotoMode] = useState(false);

    return(
        <Tab.Pane>
            <Grid>
                <Grid.Column width={16}>
                    <Header floated='left' icon='image' content='Photo' />
                    {isCurrentUser && (
                        <Button floated='right' basic content={addPhotoMode? 'Cancel' : 'Add photo'} 
                        onClick={() => setAddPhotoMode(!addPhotoMode)}
                        />
                    )}
                </Grid.Column>
                <Grid.Column width={16}>
                    {addPhotoMode ? 
                        (<PhotoUploadWidget/>) :
                        (

                            <Card>
                                <Image src={profile.photo?.url || '/assets/user.png'} />
                            </Card>
                        )
                    }
                </Grid.Column>
            </Grid>
            
          
        </Tab.Pane>
    )


})