import React, { useState, useEffect, Fragment } from "react";
import { Container } from "semantic-ui-react";
import axios from "axios";
import { IPost } from "../models/post";
import NavBar from "../../features/nav/NavBar";
import PostDashboard from "../../features/posts/dashboard/PostDashboard";

const App = () => {
  const [posts, setPosts] = useState<IPost[]>([]);
  const [selectedPost, setSelectedPost] = useState<IPost | null>(null);
  const [editMode, setEditMode] = useState(false);

  const handleSelectPost = (id: string) => {
    setSelectedPost(posts.filter(p => p.id === id)[0]);
    setEditMode(false);
  };

  const handleOpenCreateForm = () => {
    setSelectedPost(null);
    setEditMode(true);
  };

  const handleCreatePost = (post: IPost) => {
    setPosts([...posts, post]);
    setSelectedPost(post);
    setEditMode(false);
  };
  const handleEditPost = (post: IPost) => {
    setPosts([...posts.filter(p => p.id !== post.id), post]);
    setSelectedPost(post);
    setEditMode(false);
  };

  const handleDeletePost = (id: string) => {
    setPosts([...posts.filter(p => p.id !== id)]);
  };

  useEffect(() => {
    axios.get<IPost[]>("http://localhost:5000/api/posts").then(response => {
      setPosts(response.data);
    });
  }, []);
  return (
    <Fragment>
      <NavBar openCreateForm={handleOpenCreateForm} />
      <Container style={{ marginTop: "7em" }}>
        <PostDashboard
          posts={posts}
          selectPost={handleSelectPost}
          selectedPost={selectedPost}
          editMode={editMode}
          setEditMode={setEditMode}
          setSelectedPost={setSelectedPost}
          createPost={handleCreatePost}
          editPost={handleEditPost}
          deletePost={handleDeletePost}
        />
      </Container>
    </Fragment>
  );
};

export default App;
