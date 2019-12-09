import React from "react";
import { Item, Button, Segment, Icon } from "semantic-ui-react";
import { Link } from "react-router-dom";
import { IPost } from "../../../app/models/post";



const PostListItem: React.FC<{ post: IPost }> = ({ post }) => {
  return (
    <Segment.Group>
      <Segment>
        <Item.Group>
          <Item>
            <div className ="user-info-box">
            <Item.Image
              src='/assests/user.png'
              size='tiny'
              circular
            />
            Deepak
            </div>
           
            <Item.Content>
              <Item.Header as='a'>{post.heading}</Item.Header>
              <Item.Description>Deepak</Item.Description>
            </Item.Content>
          </Item>
        </Item.Group>
      </Segment>
      <Segment>
        <Icon name='clock' /> {post.date}
        <Icon name='marker' /> {post.date}
      </Segment>
      <Segment secondary>Likes will go here.</Segment>
      <Segment clearing>
        <span>{post.description}</span>
        <Button
          as={Link}
          to={`/posts/${post.id}`}
          floated={"right"}
          content='View'
          color='blue'
        />
      </Segment>
    </Segment.Group>
  );
};

export default PostListItem;
