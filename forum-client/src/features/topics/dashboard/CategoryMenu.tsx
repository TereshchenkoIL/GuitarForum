import { observer } from 'mobx-react-lite'
import React, { Component, useEffect, useState } from 'react'
import { Menu, Segment } from 'semantic-ui-react'
import LoadingComponent from '../../../app/layout/LoadingComponent'
import Category from '../../../app/models/category'
import { useStore } from '../../../app/stores/store'

export default observer( function CategoryMenu() {
 
    const [activeItem, setActiveItem] = useState("all")

    const {categoryStore, topicStore} = useStore();

    const{loadCategories, loadingInitial, Categories} = categoryStore;
   function handleItemClick(category: Category){
        setActiveItem(category.name)
        topicStore.setCategory(category);
        topicStore.setLoadByCategory(true);
   }

   function handleAllClick(){
    topicStore.setLoadByCategory(false);
    }

   useEffect(() => {
       loadCategories(); 
   }, [loadCategories])
    
    return (
      <div>
        <Menu pointing secondary>
          <Menu.Item
            name='all'
            active={activeItem === 'all'}
            onClick={() => handleAllClick()}
          />
          {loadingInitial ? 
         <LoadingComponent content='Loading...'/>:
            Categories.map(category => (
                <Menu.Item
                key={category.id}
                name={category.name}
                active={activeItem === category.name}
                onClick={() => handleItemClick(category)}
              />
            ))
        }
         
        </Menu>

      </div>
    )
  }
);