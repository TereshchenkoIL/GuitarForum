import { observer } from "mobx-react-lite";
import React from "react";
import { Link } from "react-router-dom";
import { Button, Item, Segment } from "semantic-ui-react";
import Category from "../../../app/models/category";
import { useStore } from "../../../app/stores/store";



interface Props{
    category: Category;
}

export default observer( function CategoryListItem({category} : Props)
{
    const {categoryStore, userStore:{isAdmin, user}} = useStore();

    
    return (
        <Segment.Group>
            <Segment>
            
                <Item.Group>
                    <Item key={category.id}>
                        <Item.Content>
                            <Item.Header >
                                {category.name}
                            </Item.Header>
                    
                        </Item.Content>
                    </Item>
                </Item.Group>
            </Segment>

            
            <Segment clearing>

                {( isAdmin) && (
                    <Button 
                    onClick={() => categoryStore.deleteCategory(category.id)}
                    color='red'
                    floated='right'
                    content='Delete' />
                )}
                

                <Button 
                    as={Link}
                    to={`/editCategory/${category.id}`}
                    color='teal'
                    floated='right'
                    content='Edit' />
            </Segment>
        </Segment.Group>
    );
})