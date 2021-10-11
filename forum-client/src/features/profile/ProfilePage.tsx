import { observer } from "mobx-react-lite";
import React, { useEffect } from "react";
import { useParams } from "react-router";
import LoadingComponent from "../../app/layout/LoadingComponent";
import { Profile } from "../../app/models/profile";
import { useStore } from "../../app/stores/store";
import ProfileContent from "./ProfileContent";
import ProfileHeader from "./ProfileHeader";

export default observer( function ProfilePage(){
    const{profileStore} = useStore();

    const {username} = useParams<{username: string}>();

    const{loadingProfile, loadProfile, profile} = profileStore;

    useEffect(() => {
        loadProfile(username)
        
    }, [loadProfile,username])

    if(loadingProfile) return <LoadingComponent content='Loading profile...' />
    
    return(
        <>
            {profile && 
            <> 
                <ProfileHeader profile={profile!}/>
                <ProfileContent profile={profile!} />
            </>}
          
        </>
    )
}
)