import axios from "axios";
import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { Space, Table } from "antd";
import "../Components/Css/button.css";
import jwt_decode from "jwt-decode";
import AxiosClient from "../Components/axiosClient";

const { Column, ColumnGroup } = Table;

function Books() {
    let token = localStorage.getItem("access-token");
    const decoded = jwt_decode(token);

    const [book, setBook] = useState([]);

    const navigate = useNavigate();

    const handleUpdate = (id) => {
        navigate(`/book/update/${id}`);
    };

    const handleAdd = () => {
        navigate(`/book/create`);
    };

    const deleteABook = async (id) => {
        await AxiosClient.delete(`/book/${id}`);
    };

    const handleDelete = (id) => {
        var checkingDelete = window.confirm(
            `Do you want to delete book with id: ${id}`
        );
        if (checkingDelete) {
            deleteABook(id);

            window.location.reload();
        }
    };

    const getAllBooks = async () => {
        let response = await AxiosClient.get("/book");
        setBook(response.data);
    };

    useEffect(() => {
        getAllBooks();
    }, []);

    return (
        <div>
            <div>
                {decoded[
                    "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
                ] === "SuperAdmin" && (
                        <div class="text-center">
                            <button class="createButton" onClick={handleAdd}>
                                Add Book
                            </button>
                        </div>
                    )}

                <div>
                    <Table dataSource={book} pagination={{ pageSize: 10 }} key="1">
                        <Column
                            title="Book Id"
                            dataIndex="bookId"
                            sorter={(a, b) => a.bookId - b.bookId}
                            key={book.bookId}
                        />
                        <Column title="Title" dataIndex="title" key={book.title} />
                        <ColumnGroup title="Category">
                            <Column
                                title="Category Id"
                                key={book.categoryId}
                                render={(_, record) => <p>{record.category.categoryId}</p>}
                            />
                            <Column
                                title="Category Name"
                                key={book.name}
                                render={(_, record) => <p>{record.category.name}</p>}
                            />
                        </ColumnGroup>
                        {decoded[
                            "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
                        ] === "SuperAdmin" && (
                                <Column
                                    title="Action"
                                    key="action"
                                    render={(_, record) => (
                                        <Space size="middle">
                                            <button
                                                class="updateButton"
                                                onClick={() => handleUpdate(record.bookId)} > Edit {record.firstName}
                                            </button>
                                            <button
                                                class="deleteButton"
                                                value={record.bookId}
                                                onClick={() => handleDelete(record.bookId)} > Delete
                                            </button>
                                        </Space>
                                    )}
                                />
                            )}
                    </Table>
                </div>
            </div>
        </div>
    );
}

export default Books;