import React, { useContext, useEffect } from "react";
import { Card, Image, Button } from "semantic-ui-react";
import { observer } from "mobx-react-lite";
import PostStore from "../../../app/stores/postStore";
import { RouteComponentProps } from "react-router";
import LoadingComponent from "../../../app/layout/LoadingComponent";
import { Link } from "react-router-dom";

interface DetailParams {
  id: string;
}

const PostDetails: React.FC<RouteComponentProps<DetailParams>> = ({
  match,
  history
}) => {
  const postStore = useContext(PostStore);
  const {
    post,
    loadPost,
    loadingInitial
  } = postStore;

  useEffect(() => {
    loadPost(match.params.id);
  }, [loadPost, match.params.id]);

  if (loadingInitial || !post)
    return <LoadingComponent content='Loading Post..' />;
  return (
    <Card fluid>
      <Image src='/assests/placeholder.png' />
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
            as={Link}
            to={`/manage/${post.id}`}
            basic
            color='blue'
            content='Edit'
          ></Button>
          <Button
            onClick={() => history.push("/posts")}
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
