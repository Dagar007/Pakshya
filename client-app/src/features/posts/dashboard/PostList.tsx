import React, { SyntheticEvent } from "react";
import { Item, Button, Label, Segment } from "semantic-ui-react";
import { IPost } from "../../../app/models/post";

interface IProps {
  posts: IPost[];
  selectPost: (id: string) => void;
  deletePost: (event: SyntheticEvent<HTMLButtonElement>, id: string) => void;
  submitting: boolean;
  target: string;
}

const PostList: React.FC<IProps> = ({
  posts,
  selectPost,
  deletePost,
  submitting,
  target
}) => {
  return (
    <Segment clearing>
      <Item.Group divided>
        {posts.map(post => (
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
                  onClick={() => selectPost(post.id)}
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

export default PostList;
