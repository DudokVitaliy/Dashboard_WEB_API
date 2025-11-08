import { Route, Routes } from 'react-router';
import DefaultLayout from './components/layout/DefaultLayout';
import MainPage from './components/mainPage/mainPage';
import LoginPage from './pages/loginPage/LoginPage';
import { useEffect } from 'react';
import { useDispatch } from 'react-redux';
import { loginSucess } from './store/slices/authSlice';
import RegisterPage from './pages/registerPage/RegisterPage';
function App() {

  const dispatch = useDispatch();
  useEffect(() => {
    const token = localStorage.getItem("token");
    if (token) {
      dispatch(loginSucess(token));
    }

  }, []);

  return (
    <>
      <Routes>
        <Route element={<DefaultLayout />}>
          <Route index element={<MainPage />} />
          <Route path='/login' element={<LoginPage />} />
          <Route path='/register' element={<RegisterPage />} />
        </Route>
      </Routes>
    </>
  )
}

export default App
