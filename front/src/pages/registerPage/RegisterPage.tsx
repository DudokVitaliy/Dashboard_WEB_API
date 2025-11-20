import * as React from 'react';
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import Divider from '@mui/material/Divider';
import FormLabel from '@mui/material/FormLabel';
import FormControl from '@mui/material/FormControl';
import TextField from '@mui/material/TextField';
import Typography from '@mui/material/Typography';
import Stack from '@mui/material/Stack';
import MuiCard from '@mui/material/Card';
import { styled } from '@mui/material/styles';
import { useFormik } from 'formik';
import * as Yup from 'yup';
import axios from 'axios';
import { apiBaseUrl } from '../../env';
import type { ServiceResponse } from './services/types';
import { SitemarkIcon, GoogleIcon, FacebookIcon } from './components/CustomeIcons';
import { Link, useNavigate } from 'react-router';

const Card = styled(MuiCard)(({ theme }) => ({
  display: 'flex',
  flexDirection: 'column',
  alignSelf: 'center',
  width: '100%',
  padding: theme.spacing(4),
  gap: theme.spacing(2),
  margin: 'auto',
  [theme.breakpoints.up('sm')]: {
    maxWidth: '450px',
  },
  boxShadow:
    'hsla(220, 30%, 5%, 0.05) 0px 5px 15px 0px, hsla(220, 25%, 10%, 0.05) 0px 15px 35px -5px',
  ...theme.applyStyles('dark', {
    boxShadow:
      'hsla(220, 30%, 5%, 0.5) 0px 5px 15px 0px, hsla(220, 25%, 10%, 0.08) 0px 15px 35px -5px',
  }),
}));

const SignUpContainer = styled(Stack)(({ theme }) => ({
  height: 'calc((1 - var(--template-frame-height, 0)) * 100dvh)',
  minHeight: '100%',
  padding: theme.spacing(2),
  [theme.breakpoints.up('sm')]: {
    padding: theme.spacing(4),
  },
  '&::before': {
    content: '""',
    display: 'block',
    position: 'absolute',
    zIndex: -1,
    inset: 0,
    backgroundImage:
      'radial-gradient(ellipse at 50% 50%, hsl(210, 100%, 97%), hsl(0, 0%, 100%))',
    backgroundRepeat: 'no-repeat',
    ...theme.applyStyles('dark', {
      backgroundImage:
        'radial-gradient(at 50% 50%, hsla(210, 100%, 16%, 0.5), hsl(220, 30%, 5%))',
    }),
  },
}));

// Валідація за допомогою Yup
const validationSchema = Yup.object({
  email: Yup.string()
    .email("Невірний формат пошти")
    .required("Поле 'email' є обов'язковим"),
  userName: Yup.string()
    .required("Поле 'userName' є обов'язковим")
    .min(3, 'Мінімум 3 символи'),
  password: Yup.string()
    .required("Поле 'password' є обов'язковим")
    .min(6, 'Мінімальна довжина паролю 6 символів'),
  firstName: Yup.string(),
  lastName: Yup.string(),
});

const RegisterPage = () => {
  const navigate = useNavigate();
  const [loading, setLoading] = React.useState(false);
  const [error, setError] = React.useState<string | null>(null);
  const [success, setSuccess] = React.useState<string | null>(null);

  const handleSubmit = async (values: any) => {
    try {
      setLoading(true);
      setError(null);
      setSuccess(null);

      const response = await axios<ServiceResponse>({
        url: `${apiBaseUrl}/auth/register`,
        method: "post",
        data: values,
      });

      if (response.data.isSuccess !== false) {
        setSuccess("Реєстрація успішна! Перевірте вашу пошту для підтвердження.");
        setTimeout(() => navigate('/login'), 3000);
      } else {
        setError(response.data.message);
      }
    } catch (err: any) {
      if (err.response) {
        setError(err.response.data?.message || "Помилка реєстрації");
      } else {
        setError("Помилка мережі");
      }
    } finally {
      setLoading(false);
    }
  };

  const formik = useFormik({
    initialValues: {
      email: '',
      userName: '',
      password: '',
      firstName: '',
      lastName: '',
    },
    validationSchema,
    onSubmit: handleSubmit,
  });

  return (
    <SignUpContainer direction="column" justifyContent="space-between">
      <Card variant="outlined">
        <SitemarkIcon />
        <Typography
          component="h1"
          variant="h4"
          sx={{ width: '100%', fontSize: 'clamp(2rem, 10vw, 2.15rem)' }}
        >
          Sign up
        </Typography>
        <Box
          component="form"
          onSubmit={formik.handleSubmit}
          noValidate
          sx={{
            display: 'flex',
            flexDirection: 'column',
            width: '100%',
            gap: 2,
          }}
        >
          <FormControl>
            <FormLabel htmlFor="email">Email</FormLabel>
            <TextField
              id="email"
              name="email"
              type="email"
              placeholder="example@gmail.com"
              fullWidth
              variant="outlined"
              color="primary"
              value={formik.values.email}
              onChange={formik.handleChange}
              onBlur={formik.handleBlur}
              error={formik.touched.email && Boolean(formik.errors.email)}
              helperText={formik.touched.email && formik.errors.email}
            />
          </FormControl>

          <FormControl>
            <FormLabel htmlFor="userName">User Name</FormLabel>
            <TextField
              id="userName"
              name="userName"
              type="text"
              placeholder="username"
              fullWidth
              variant="outlined"
              color="primary"
              value={formik.values.userName}
              onChange={formik.handleChange}
              onBlur={formik.handleBlur}
              error={formik.touched.userName && Boolean(formik.errors.userName)}
              helperText={formik.touched.userName && formik.errors.userName}
            />
          </FormControl>

          <FormControl>
            <FormLabel htmlFor="password">Пароль</FormLabel>
            <TextField
              id="password"
              name="password"
              type="password"
              placeholder="••••••"
              fullWidth
              variant="outlined"
              color="primary"
              value={formik.values.password}
              onChange={formik.handleChange}
              onBlur={formik.handleBlur}
              error={formik.touched.password && Boolean(formik.errors.password)}
              helperText={formik.touched.password && formik.errors.password}
            />
          </FormControl>

          <FormControl>
            <FormLabel htmlFor="firstName">Ім'я (необов’язково)</FormLabel>
            <TextField
              id="firstName"
              name="firstName"
              type="text"
              placeholder="Ім'я"
              fullWidth
              variant="outlined"
              color="primary"
              value={formik.values.firstName}
              onChange={formik.handleChange}
            />
          </FormControl>

          <FormControl>
            <FormLabel htmlFor="lastName">Прізвище (необов’язково)</FormLabel>
            <TextField
              id="lastName"
              name="lastName"
              type="text"
              placeholder="Прізвище"
              fullWidth
              variant="outlined"
              color="primary"
              value={formik.values.lastName}
              onChange={formik.handleChange}
            />
          </FormControl>

          {error && (
            <Typography color="error" variant="body2" sx={{ mt: 1 }}>
              {error}
            </Typography>
          )}
          {success && (
            <Typography color="success.main" variant="body2" sx={{ mt: 1 }}>
              {success}
            </Typography>
          )}

          <Button
            type="submit"
            fullWidth
            variant="contained"
            disabled={loading}
          >
            {loading ? 'Зачекайте...' : 'Зареєструватись'}
          </Button>
        </Box>

        <Divider>or</Divider>
        <Box sx={{ display: 'flex', flexDirection: 'column', gap: 2 }}>
          <Button
            fullWidth
            variant="outlined"
            onClick={() => alert('Sign up with Google')}
            startIcon={<GoogleIcon />}
          >
            Sign up with Google
          </Button>
          <Button
            fullWidth
            variant="outlined"
            onClick={() => alert('Sign up with Facebook')}
            startIcon={<FacebookIcon />}
          >
            Sign up with Facebook
          </Button>
          <Typography sx={{ textAlign: 'center' }}>
            Вже маєте акаунт?{' '}
            <Link to="/login">
              Увійти
            </Link>
          </Typography>
        </Box>
      </Card>
    </SignUpContainer>
  );
};

export default RegisterPage;
