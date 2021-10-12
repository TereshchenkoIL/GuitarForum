import { observer } from 'mobx-react-lite'
import React, { Component, useEffect, useState } from 'react'
import { Menu, Segment } from 'semantic-ui-react'
import LoadingComponent from '../../../app/layout/LoadingComponent'
import Category from '../../../app/models/category'
import { PagingParams } from '../../../app/models/pagination'
import { useStore } from '../../../app/stores/store'

export default observer( function CategoryMenu() {
 
    const [activeItem, setActiveItem] = useState("all")

    const {categoryStore, topicStore} = useStore();

    const{loadCategories, loadingInitial, Categories} = categoryStore;

    const{loadAllTopics, topicRegistry, loadTopicsByCaetgory, setPagingParams} = topicStore;

   function handleItemClick(category: Category){
        setPagingParams(new PagingParams())
        setActiveItem(category.name)
        topicStore.setCategory(category);
        topicStore.setLoadByCategory(true);
        topicStore.topicRegistry.clear();
        loadTopicsByCaetgory();
   }

   function handleAllClick(){
      setPagingParams(new PagingParams())
      topicStore.topicRegistry.clear();
      setActiveItem('all')
      topicStore.setCategory(null);
      topicStore.setLoadByCategory(false);
      loadAllTopics()
    }

   useEffect(() => {
       loadCategories(); 
   }, [loadCategories])
    
    return (
      <div>
        <Menu pointing secondary>
          <Menu.Item
            key='all'
            name='all'
            active={activeItem === 'all'}
            onClick={() => handleAllClick()}
            disabled={topicStore.loadingInitial}
          />
          {loadingInitial ? 
         <LoadingComponent content='Loading...'/>:
            Categories.map(category => (
                <Menu.Item
                key={category.id}
                name={category.name}
                active={activeItem === category.name}
                onClick={() => handleItemClick(category)}
                disabled={topicStore.loadingInitial}
              />
            ))
        }
         
        </Menu>

      </div>
    )
  }
);