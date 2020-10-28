import { useHistory, useParams } from "react-router-dom";
import { UserProfileContext } from "../../providers/UserProfileProvider";
import React, { useContext, useEffect } from "react";
import { Card, CardImg, CardBody, Row, Col, Button } from "reactstrap";

const UserProfileDetails = () => {

    const { id } = useParams();
    const history = useHistory();
    const { userProfile, singleUser, getUserProfileById, updateUserProfile } = useContext(UserProfileContext);
    const currentUser = JSON.parse(sessionStorage.getItem('userProfile')).id;

    useEffect(() => {
        getUserProfileById(id);
    }, [id]);

    console.log(singleUser, "current user Id")

    return (
        <>
            <Card className="m-4">
                <Row margin="m-4">
                    <h1 ><strong>{singleUser.firstName}{singleUser.lastName}</strong></h1>
                </Row>
                <Row>
                    <Col sm="6">
                        <h3>{singleUser.bio}</h3>
                    </Col>
                </Row>
                <Row margin="m-4">
                </Row>
                <CardBody>
                    <CardImg className="userProfileDetailImg" top src={singleUser.imageLocation} alt={singleUser.username} />
                </CardBody>

                {(currentUser === singleUser.id) ?

                    <div>
                        <Button onClick={() => history.push(`/userProfile/edit/${singleUser.id}`)}>Edit</Button>
                        <Button onClick={() => history.push(`/userProfile/delete/${singleUser.id}`)}>Delete</Button>
                    </div>
                    :
                    null}

            </Card>
        </>


    );
}

export default UserProfileDetails;