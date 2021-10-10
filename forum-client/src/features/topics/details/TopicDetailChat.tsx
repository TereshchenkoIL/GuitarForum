import { formatDistanceToNow } from "date-fns";
import { Formik, Field, FieldProps } from "formik";
import { observer } from "mobx-react-lite";
import React, { useEffect } from "react";
import { Link } from "react-router-dom";
import { Form, Header, Loader, Segment, Comment } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";
import * as Yup from 'yup'

interface Props{
    topicId: string;
}

export default observer(function TopicDetailChat({topicId}: Props){
    const {commentStore} = useStore();

    useEffect(() => {
        if (topicId) {
            commentStore.createHubConnection(topicId)
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
                    <Formik
                        onSubmit={(values, { resetForm }) => commentStore.addComent(values).then(() => resetForm())}
                        initialValues={{ body: '' }}
                        validationSchema={Yup.object({
                            body: Yup.string().required()
                        })}
                    >
                        {({ isSubmitting, isValid, handleSubmit }) => (
                            <Form className='ui form'>
                                <Field name='body'>
                                    {(props: FieldProps) => (
                                        <div style={{ position: 'relative' }}>
                                            <Loader active={isSubmitting} />
                                            <textarea
                                                placeholder='Enter your comment (Enter to submit, SHIFT + enter for new line'
                                                rows={2}
                                                {...props.field}
                                                onKeyPress={e => {
                                                    if (e.key === 'Enter' && e.shiftKey)
                                                        return;
                                                    if (e.key === 'Enter' && !e.shiftKey) {
                                                        e.preventDefault();
                                                        isValid && handleSubmit();
                                                    }
                                                } } />
                                        </div>
                                    )}
                                </Field>
                            </Form>
                        )}

                    </Formik>

                    <Comment.Group>
                        {
                        Array.from(commentStore.comments.values()).sort((a, b) => b.createdAt!.getTime() - a.createdAt!.getTime()).map(comment => (
                            <Comment key={comment.id}>
                                <Comment.Avatar src={comment.image || '/assets/user.png'} />
                                <Comment.Content>
                                    <Comment.Author as={Link} to={`/profiles/${comment.username}`}>
                                        {comment.displayName}
                                    </Comment.Author>
                                    <Comment.Metadata>
                                        <div>{formatDistanceToNow(comment.createdAt) + ' ago'}</div>
                                    </Comment.Metadata>
                                    <Comment.Text style={{ whiteSpace: 'pre-wrap' }}>{comment.body}</Comment.Text>
                                    <Comment.Actions>
                                        <Comment.Action>Reply</Comment.Action>
                                    </Comment.Actions>
                                </Comment.Content>
                            </Comment>
                        ))}


                    </Comment.Group>
                </Segment>
            </>

    );
})