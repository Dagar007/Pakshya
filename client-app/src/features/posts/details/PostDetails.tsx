import React from "react";
import { Card, Image, Button } from "semantic-ui-react";
import { IPost } from "../../../app/models/post";

interface IProps {
  post: IPost;
  setEditMode: (editMode: boolean) => void;
  setSelectedPost: (post: IPost | null) => void;
}

const PostDetails: React.FC<IProps> = ({
  post,
  setEditMode,
  setSelectedPost
}) => {
  return (
    <Card fluid>
      <Image src='/assests/placeholder.png' wrapped ui={false} />
      <Card.Content>
        <Card.Header>{post.heading}</Card.Header>
        <Card.Meta>
          <span>{post.date}</span>
          <span>{post.category}</span>
        </Card.Meta>
        <Card.Description>{post.description}</Card.Description>
      </Card.Content>
      <Button.Group extra>
        <Button.Group widths={2}>
          <Button
            onClick={() => setEditMode(true)}
            basic
            color='blue'
            content='Edit'
          ></Button>
          <Button
            onClick={() => setSelectedPost(null)}
            basic
            color='grey'
            content='Cancel'
          ></Button>
        </Button.Group>
      </Button.Group>
    </Card>
  );
};

export default PostDetails;
