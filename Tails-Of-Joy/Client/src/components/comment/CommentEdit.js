import React, { useContext, useState, useEffect } from "react";
import { CommentContext } from "../../providers/CommentProvider"
import { useHistory, useParams } from "react-router-dom"
import { Button, Form, FormGroup, Label, Input, Card, CardBody } from "reactstrap";
import { PostContext } from "../../providers/PostProvider";


const CommentEdit = () => {

    const { comment, getCommentById, updateComment } = useContext(CommentContext)
    const history = useHistory();
    const [editedComment, setEditedComment] = useState({});
    const { id } = useParams();
    const { post } = useContext(PostContext)

    const user = JSON.parse(sessionStorage.getItem("userProfile")).id
    post.userProfileId = user


    useEffect(() => {
        getCommentById(id)
    }, [])

    const handleFieldChange = (e) => {
        const stateToChange = { ...editedComment }
        stateToChange[e.target.id] = e.target.value;
        setEditedComment(stateToChange);
    };

    const saveUpdatedComment = (e) => {
        e.preventDefault();
        updateComment(editedComment)
        history.push(`/post/details/${editedComment.postId}`)

    };

    useEffect(() => {
        setEditedComment(comment)
    }, [comment])

    const Cancel = () => {
        history.push(`/post/details/${comment.postId}`)
    }

    return (
        <>
            <div className="container pt-4">
                <div className="row justify-content-center">
                    <Card className="col-sm-12 col-lg-6">
                        <CardBody>
                            <h5 align="center">Edit Comment</h5>
                            <FormGroup>
                                <Input
                                    className="newComment"
                                    onChange={handleFieldChange}
                                    type="textarea"
                                    style={{ height: 200 }}
                                    id="Content"
                                    defaultValue={comment.content}
                                />
                            </FormGroup>
                            <Button
                                onClick={saveUpdatedComment}
                                variant="custom"
                                type="submit">
                                Save
                    </Button>&nbsp;




                            <Button onClick={Cancel}>Cancel</Button>
                        </CardBody>
                    </Card>
                </div>
            </div>
        </>
    )

}

export default CommentEdit