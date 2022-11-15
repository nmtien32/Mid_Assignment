import { useMemo, useState } from "react";
import { Grid, Paper, TextField, Button } from "@mui/material";
import { useNavigate } from "react-router-dom";
import { useAuthContext } from "../Hooks/authContext";
import axios from "axios";

const initialValues = {
    userName: "",
    password: "",
    read: false,
};

const required = (value) => {
    if (value === null || value === undefined || value === "") {
        return "this field is required";
    }
    return "";
};

const minLength = (value, length) => {
    if (value.length < length) return `At least ${length} characters`;
    return "";
};

const getUserNameError = (value) => {
    return (
        required(value)
    );
};

const getPasswordError = (value) => {
    return required(value) || minLength(value, 2);
};

const Login = () => {
    let navigate = useNavigate();
    const { setIsAuthenticated } = useAuthContext();

    const [status, setStatus] = useState();

    const login = () => {
        axios({
            method: "post",
            url: "https://localhost:7233/api/user/login",
            data: {
                userName: values.userName,
                password: values.password
            }

        })
            .then((response) => {
                localStorage.setItem("access-token", response.data)
                console.log(response.data)
            })
            .then(() => {
                if (localStorage.getItem("access-token") !== null) {
                    setIsAuthenticated(true);
                    navigate("/");
                }
            })
            .catch((error) => {
                if (error.response) {
                    setStatus("Wrong username or password")
                }
            })
    };

    const [values, setValues] = useState(initialValues);

    const userNameError = useMemo(() => {
        return getUserNameError(values.userName);
    }, [values.userName]);

    const passwordError = useMemo(() => {
        return getPasswordError(values.password);
    }, [values.password]);

    const isFormValid = !userNameError && !passwordError;

    const handleOnChange = (event) => {
        const newValues = {
            ...values,
            [event.target.name]:
                event.target.name === "read"
                    ? event.target.checked
                    : event.target.value,
        };
        setValues(newValues);
    };
    const paperStyle = {
        padding: 20,
        height: "70vh",
        width: 300,
        margin: "20px auto",
    };
    return (
        <Grid>
            <Paper elevation={10} style={paperStyle}>
                <Grid align="center">
                    <h2>Sign in</h2>
                </Grid>
                <TextField
                    value={values.userName}
                    onChange={handleOnChange}
                    name="userName"
                    placeholder="Enter Username"
                />
                <TextField
                    value={values.password}
                    onChange={handleOnChange}
                    name="password"
                    placeholder="Enter Password"
                />
                <Button className="loginButton"
                    type="button"
                    onClick={login}
                    variant="contained"
                > Log in
                </Button>
                <h2 style={{ color: '#d50000' }}>{status}</h2>
            </Paper>
        </Grid>
    );
};

export default Login;