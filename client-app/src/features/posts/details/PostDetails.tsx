import React, { useContext } from "react";
import { Card, Image, Button } from "semantic-ui-react";
import { observer } from "mobx-react-lite";
import PostStore from "../../../app/stores/postStore";


const PostDetails: React.FC = () => {
  const postStore = useContext(PostStore);
  const {selectedPost: post, openEditForm, cancelSelectedPost} = postStore;
  return (
    <Card fluid>
      <Image src='/assests/placeholder.png'  />
      <Card.Content>
        <Card.Header>{post!.heading}</Card.Header>
        <Card.Meta>
          <span>{post!.date}</span>
          <span>{post!.category}</span>
        </Card.Meta>
        <Card.Description>{post!.description}</Card.Description>
      </Card.Content>
      <Button.Group>
        <Button.Group widths={2}>
          <Button
            onClick={() => openEditForm(post!.id)}
            basic
            color='blue'
            content='Edit'
          ></Button>
          <Button
            onClick={cancelSelectedPost}
            basic
            color='grey'
            content='Cancel'
          ></Button>
        </Button.Group>
      </Button.Group>
    </Card>
  );
};

export default observer(PostDetails);
