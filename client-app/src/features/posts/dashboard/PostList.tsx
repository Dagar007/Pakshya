import React, { useContext } from "react";
import { Item, Button, Label, Segment } from "semantic-ui-react";
import { observer } from "mobx-react-lite";
import PostStore from "../../../app/stores/postStore";
import { Link } from "react-router-dom";


const PostList: React.FC = () => {
  const postStore = useContext(PostStore);
  const {postByDate, deletePost, submitting, target} = postStore;

  return (
    <Segment clearing>
      <Item.Group divided>
        {postByDate.map(post => (
          <Item key={post.id}>
            <Item.Image
              src='https://material.angular.io/assets/img/examples/shiba2.jpg'
              size='tiny'
              rounded
            />

            <Item.Content>
              <Item.Header as='a'>{post.heading}</Item.Header>
              <Item.Meta>{post.date}</Item.Meta>
              <Item.Description>
                <div>{post.description}</div>
                <div>City</div>
                {/* <Image src='/images/wireframe/short-paragraph.png' /> */}
              </Item.Description>
              <Item.Extra>
                <Button
                  as={Link} to={`/posts/${post.id}`}
                  floated={"right"}
                  content='View'
                  color='blue'
                />
                <Button
                  name={post.id}
                  loading={target === post.id && submitting}
                  onClick={e => deletePost(e, post.id)}
                  floated={"right"}
                  content='Delete'
                  color='red'
                />
                <Label basic content={post.category}></Label>
              </Item.Extra>
            </Item.Content>
          </Item>
        ))}
      </Item.Group>
    </Segment>
  );
};

export default observer(PostList);
