import { useState } from 'react'
import axios from "axios";
import { Routes, Route, useNavigate, Navigate, Link } from 'react-router-dom';
import './App.css'

function Login() {
    const [correo, setCorreo] = useState('');
    const [password, setPassword] = useState('');
    const navigate = useNavigate();

    const handleLogin = async (e) => {
        e.preventDefault();

        try {
            const response = await axios.post('http://localhost:5042/api/auth/login', {
                correoAdmin: correo,
                password: password
            });

            localStorage.setItem('token', response.data.token);
            alert('Login exitoso');
            navigate('/test');
        } catch (error) {
            console.error('Error login:', error.response ? error.response.data : error.message);
            alert('Error al iniciar sesión. Revisa la consola.');
        }
    };

    return (
        <div>
            <h2>Login</h2>
            <form onSubmit={handleLogin}>
                <input
                    type="email"
                    placeholder="Correo"
                    value={correo}
                    onChange={(e) => setCorreo(e.target.value)}
                    required
                />
                <input
                    type="password"
                    placeholder="Contraseña"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                    required
                />
                <button type="submit">Iniciar sesión</button>
            </form>
            <p>¿No tienes cuenta? <Link to="/register">Regístrate aquí</Link></p>
        </div>
    );
}

function Register() {
    const [nombreComercial, setNombreComercial] = useState('');
    const [correo, setCorreo] = useState('');
    const [password, setPassword] = useState('');
    const navigate = useNavigate();

    const handleRegister = async (e) => {
        e.preventDefault();

        try {
            const response = await axios.post('http://localhost:5042/api/auth/register', {
                nombreComercial: nombreComercial,
                correoAdmin: correo,
                password: password
            });

            localStorage.setItem('token', response.data.token);
            alert('Registro exitoso');
            navigate('/test');
        } catch (error) {
            console.error('Error registro:', error.response ? error.response.data : error.message);
            alert('Error al registrar. Revisa la consola.');
        }
    };

    return (
        <div>
            <h2>Registro</h2>
            <form onSubmit={handleRegister}>
                <input
                    type="text"
                    placeholder="Nombre Comercial"
                    value={nombreComercial}
                    onChange={(e) => setNombreComercial(e.target.value)}
                    required
                />
                <input
                    type="email"
                    placeholder="Correo"
                    value={correo}
                    onChange={(e) => setCorreo(e.target.value)}
                    required
                />
                <input
                    type="password"
                    placeholder="Contraseña"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                    required
                />
                <button type="submit">Registrarse</button>
            </form>
            <p>¿Ya tienes cuenta? <Link to="/">Inicia sesión aquí</Link></p>
        </div>
    );
}

function TestPage() {
    const token = localStorage.getItem('token');
    const navigate = useNavigate();
  
    const handleLogout = () => {
      localStorage.removeItem('token'); // 🔹 Borra el token
      navigate('/'); // 🔹 Redirige al login
    };
  
    return (
      <div>
        <h2>¡Página de Prueba!</h2>
        <p>El token existe en localStorage:</p>
        <textarea readOnly rows={4} cols={50} value={token}></textarea>
        <br />
        <button onClick={handleLogout}>Cerrar sesión</button>
      </div>
    );
  }

function PrivateRoute({ children }) {
    const token = localStorage.getItem('token');
    if (!token) {
        return <Navigate to="/" replace />;
    }
    return children;
}

function App() {
    return (
        <Routes>
            <Route path="/" element={<Login />} />
            <Route path="/register" element={<Register />} />
            <Route
                path="/test"
                element={
                    <PrivateRoute>
                        <TestPage />
                    </PrivateRoute>
                }
            />
        </Routes>
    );
}

export default App;