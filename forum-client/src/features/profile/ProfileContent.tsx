import { observer } from "mobx-react-lite";
import React from "react";
import { Tab } from "semantic-ui-react";
import { Profile } from "../../app/models/profile";
import ProfileAbout from "./ProfileAbout";
import ProfileActivity from "./ProfileActivity";
import ProfilePhoto from "./ProfilePhoto";
import ProfileTopics from "./ProfileTopics";

interface Props{
    profile: Profile
}

export default observer(function ProfileContent({profile}: Props){

    const panes = [
        {menuItem: 'About', render: () => <ProfileAbout />},
        {menuItem: 'Topics', render: () => <ProfileTopics profile={profile}/>},
        {menuItem: 'Photo', render: () => <ProfilePhoto profile={profile}/>},
        {menuItem: 'Activity', render: () => <ProfileActivity profile={profile}/>},
    ]

    return(
        <Tab
        menu={{fluid: true, vertical:true}}
        menuPosition='right'
        panes={panes}
        />
    )
})