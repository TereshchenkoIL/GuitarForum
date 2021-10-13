import { observer } from "mobx-react-lite";
import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { Card, Icon } from "semantic-ui-react";
import { Profile } from "../../app/models/profile";
import { useStore } from "../../app/stores/store";
import CalendarHeatmap from 'react-calendar-heatmap';
import LoadingComponent from "../../app/layout/LoadingComponent";
import { string } from "yup/lib/locale";
import ReactTooltip from "react-tooltip";


interface Props{
    profile: Profile
}

export default observer(function ProfileActivity({profile}: Props){

    const{profileStore} = useStore();

    const{loadActivity,  activity, loading} = profileStore;

    const [values, setValues] = useState([{}]);

    useEffect(() => {
        loadActivity(profile.username)

        ReactTooltip.rebuild();
    }, [loadActivity, profile.username])

    if(loading) return <LoadingComponent content='Loading...'/>

    return(
        
        <>
        <CalendarHeatmap
            values={activity}
            classForValue={value => {
                if (!value) {
                  return 'color-empty';
                }
                return `color-github-${value.count}`;
              }}
              tooltipDataAttrs={(value: { date:string; count: any; })=> {
                  
                return !!value.date ? {
                    
                    'data-tip': `${value.date} has count: ${
                        value.count
                      }`,
                  }: {
                    'data-tip': 'Nothing',
                  };
                }
              }

        
            />
            
            <ReactTooltip />
        </>
    )

})