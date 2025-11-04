import { Route, Routes } from 'react-router';
import DefaultLayout from './components/layout/DefaultLayout';
import MainPage from './components/mainPage/mainPage';
import LoginPage from './pages/loginPage/LoginPage';
function App() {
  return (
    <>
      <Routes>
        <Route element={<DefaultLayout />}>
          <Route index element={<MainPage />} />
          <Route path='/login' element={<LoginPage />} />
        </Route>
      </Routes>
    </>
  )
}

export default App
