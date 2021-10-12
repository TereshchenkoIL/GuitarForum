import { observer } from "mobx-react-lite";
import React, { useEffect } from "react";
import { Link } from "react-router-dom";
import { Button, Grid } from "semantic-ui-react";
import LoadingComponent from "../../../app/layout/LoadingComponent";
import { useStore } from "../../../app/stores/store";
import CategoryList from "./CategoryList";


export default observer(function CategoryDashboard(){

    const{categoryStore:{Categories, loadCategories, loadingInitial}} = useStore();

    useEffect(() => {
      loadCategories()
    }, [loadCategories])

    return(
        <Grid>
            <Grid.Column width='10'>
              
                {loadingInitial ? 
                
                <LoadingComponent content='Loading...'/> :
                    <CategoryList />
               }
            </Grid.Column>
            <Grid.Column width='2'>
            </Grid.Column>
            <Grid.Column width='3'>
                <Button as={Link} to='/createCategory' positive  content='Create Category'/>
            </Grid.Column>
        </Grid>
    )

})