import { observer } from "mobx-react-lite";
import React from "react";
import { Tab } from "semantic-ui-react";
import { Profile } from "../../app/models/profile";
import ProfilePhoto from "./ProfilePhoto";

interface Props{
    profile: Profile
}

export default observer(function ProfileContent({profile}: Props){

    const panes = [
        {menuItem: 'About', render: () => <Tab.Pane>About Content</Tab.Pane>},
        {menuItem: 'Topics', render: () => <Tab.Pane>Topics Content</Tab.Pane>},
        {menuItem: 'Photo', render: () => <ProfilePhoto profile={profile}/>},
    ]

    return(
        <Tab
        menu={{fluid: true, vertical:true}}
        menuPosition='right'
        panes={panes}
        />
    )
})