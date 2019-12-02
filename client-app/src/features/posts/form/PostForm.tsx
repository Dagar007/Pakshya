import React, { useState, FormEvent } from "react";
import { Segment, Form, Button } from "semantic-ui-react";
import { IPost } from "../../../app/models/post";
import {v4 as uuid} from "uuid"


interface IProps {
  setEditMode: (editMode: boolean) => void;
  post: IPost | null;
  createPost: (post: IPost) => void;
  editPost: (post: IPost) => void;
  submitting: boolean;
}

const PostForm: React.FC<IProps> = ({
  setEditMode,
  post: initialFormState,
  createPost,
  editPost,
  submitting
}) => {
  const initialiseForm = () => {
    if (initialFormState) {
      return initialFormState;
    } else {
      return {
        id: "",
        heading: "",
        description: "",
        category: "",
        date: "",
        url: "",
        for: 0,
        against: 0
      };
    }
  };
  const [post, setPost] = useState<IPost>(initialiseForm);

  const handleSubmit = () => {
    if(post.id.length === 0){
      let newPost = {
        ...post,
        id: uuid(),
        date : '2019-04-24T18:51:03.7828548'
      }
      createPost(newPost);
    } else {
      let editedPost = {
        ...post,
        date : '2019-04-24T18:51:03.7828548'
         //date : new Date().getFullYear()+'-'+(new Date().getMonth()+1)+'-'+new Date().getDate()
        // +' '+ new Date().getHours()+':'+ new Date().getMinutes()+':'+ new Date().getSeconds()
        //date: new Date().toString()
      }
      editPost(editedPost)
    }
  };

  const handleInputChange = (
    e: FormEvent<HTMLInputElement | HTMLTextAreaElement>
  ) => {
    const { name, value } = e.currentTarget;
    setPost({ ...post, [name]: value });
  };
  return (
    <Segment clearing>
      <Form onSubmit={handleSubmit}>
        <Form.Input
          onChange={handleInputChange}
          name='heading'
          placeholder='Heading'
          value={post.heading}
        />
        <Form.TextArea
          onChange={handleInputChange}
          name='description'
          placeholder='Description'
          rows={3}
          value={post.description}
        />
        <Form.Input
          onChange={handleInputChange}
          name='category'
          placeholder='Catgory'
          value={post.category}
        />
        <Form.Input
          onChange={handleInputChange}
          name='url'
          placeholder='Image Url (If any)'
          value={post.url}
        />
        <Button
          style={{ marginTop: 10 }}
          loading = {submitting}
          floated='right'
          positive
          type='submit'
          content='Submit'
        />
        <Button
          onClick={() => setEditMode(false)}
          style={{ marginTop: 10 }}
          floated='right'
          type='button'
          content='Cancel'
        />
      </Form>
    </Segment>
  );
};

export default PostForm;
