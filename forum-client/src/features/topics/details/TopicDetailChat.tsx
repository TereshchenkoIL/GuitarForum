import { formatDistanceToNow } from "date-fns";
import { Formik, Form, Field, FieldProps, useFormikContext } from "formik";
import { observer } from "mobx-react-lite";
import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import {  Header, Loader, Segment, Comment, Grid, Button } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";
import * as Yup from 'yup'
import { ChatComment } from "../../../app/models/comment";
import { toast } from "react-toastify";
import CommentForm from "./CommentForm";

interface Props{
    topicId: string;
}

export default observer(function TopicDetailChat({topicId}: Props){
    const {commentStore, userStore:{isAdmin, user}} = useStore();
    const{editMode, selectedComment, setEditMode, setSelectedComment} = commentStore;




    function handleEditClick(comment: ChatComment){
        setEditMode(true);
        toast.info('Type new comment')
        setSelectedComment(comment)
    }

    useEffect(() => {
        if (topicId) {
            commentStore.createHubConnection(topicId)
        }
   
        return () => {
            commentStore.clearComments();
        }
    }, [commentStore, topicId])

  

    return(

      

        <>
            <Segment
                textAlign='center'
                attached='top'
                inverted
                color='teal'
                style={{ border: 'none' }}
            >
                <Header>Conversation</Header>
            </Segment>
            <Segment attached clearing>
                <CommentForm commentBody='' />

                    <Comment.Group>
                        {
                        Array.from(commentStore.comments.values()).sort((a, b) => b.createdAt!.getTime() - a.createdAt!.getTime()).map(comment => (
                        <Grid key={comment.id}>
                            <Grid.Column floated='left' width={5}>
                                <Comment key={comment.id}>
                                    <Comment.Avatar src={comment.image || '/assets/user.png'} />
                                    <Comment.Content>
                                        <Comment.Author as={Link} to={`/profiles/${comment.username}`}>
                                            {comment.displayName}
                                        </Comment.Author>
                                        <Comment.Metadata>
                                            <div>{formatDistanceToNow(comment.createdAt) + ' ago'}</div>
                                            
                                        </Comment.Metadata>
                                        <Comment.Text style={{ whiteSpace: 'pre-wrap' }}>
                                            {editMode && selectedComment?.id === comment.id?
                                             <CommentForm commentBody={comment.body}/>:
                                          comment.body
                                           }
                                            
                                            </Comment.Text>
                                    </Comment.Content>
                                </Comment>
                            </Grid.Column>
                            <Grid.Column floated='right' width={5}  >
                                {(comment.username === user?.username || isAdmin) && (
                                     <div >
                                    <Button onClick={() => commentStore.deleteComment(comment.id)} basic icon='trash alternate outline' color='red'/>
                                    <Button onClick={() =>  handleEditClick(comment)} basic icon='pencil alternate' color='orange'/>
                                  
                                </div>
                                )}
                               
                            </Grid.Column>
                        </Grid>
                           
                         
                        ))}


                    </Comment.Group>
                </Segment>
            </>

    );
})