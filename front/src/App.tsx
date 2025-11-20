import { Route, Routes } from 'react-router';
import DefaultLayout from './components/layout/DefaultLayout';
import MainPage from './components/mainPage/mainPage';
import LoginPage from './pages/loginPage/LoginPage';
import { useEffect } from 'react';
import { useDispatch } from 'react-redux';
import { loginSucess } from './store/slices/authSlice';
import RegisterPage from './pages/registerPage/RegisterPage';
import GameListPage from './pages/gameListPage/GameListPage';
import GameDetail from './pages/gameListPage/GameDetail';
import CreateGamePage from './pages/gameListPage/CreateGamePage';
import ConfirmEmailPage from './pages/confirmEmailPage/ConfirmEmailPage';
import { useAppSelector } from "./hooks/reduxHooks";
import NotFoundPage from "./pages/notFoundPages/NotFoundPage";

function App() {

  const dispatch = useDispatch();
  const { user } = useAppSelector((state) => state.auth);

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
          <Route path="/confirmemail" element={<ConfirmEmailPage />}/>
          <Route path='/game'>
            <Route index element = {<GameListPage/>} />
            <Route path=":id" element={<GameDetail />} />
            {user && user.roles.includes("admin") && (
                <Route path="create" element={<CreateGamePage />} />
            )}
          </Route>
          <Route path="*" element={<NotFoundPage />} />
        </Route>
      </Routes>
    </>
  )
}

export default App
