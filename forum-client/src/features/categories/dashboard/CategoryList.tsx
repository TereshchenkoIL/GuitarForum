import { observer } from "mobx-react-lite";
import React from "react";
import { useStore } from "../../../app/stores/store";
import CategoryListItem from "./CategoryListItem";


export default observer( function TopicList(){

    const {categoryStore} = useStore();
    const{Categories} = categoryStore;


    
    return(
        <>
        
        {Categories.map(category => (
             <CategoryListItem key={category.id} category={category} />
         ))}
        </>
    ); 
}
)