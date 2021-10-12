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

    const {profileStore: {isCurrentUser, uploadPhoto, uploading, deletePhoto}, userStore:{isAdmin}} = useStore();
    const [addPhotoMode, setAddPhotoMode] = useState(false);

    function handlePhotoUpload(file: Blob){
        uploadPhoto(file).then(() => setAddPhotoMode(false));
    }

    return(
        <Tab.Pane>
            <Grid>
                <Grid.Column width={16}>
                    <Header floated='left' icon='image' content='Photo' />
                    {profile.photo && (isCurrentUser || isAdmin)&& (
                        <Button floated='right' basic color='red' content='Delete Photo' 
                        onClick={() => deletePhoto(profile.photo.id)}
                        />
                    )}
                    {isCurrentUser && (
                        <Button floated='right' basic content={addPhotoMode? 'Cancel' : 'Add photo'} 
                        onClick={() => setAddPhotoMode(!addPhotoMode)}
                        />
                    )}

                </Grid.Column>
                <Grid.Column width={16}>
                    {addPhotoMode ? 
                        (<PhotoUploadWidget uploadPhoto={handlePhotoUpload} loading={uploading}/>) :
                        (

                            <Card>
                                <Image src={profile.image || '/assets/user.png'} />
                            </Card>
                        )
                    }
                </Grid.Column>
            </Grid>
            
          
        </Tab.Pane>
    )


})