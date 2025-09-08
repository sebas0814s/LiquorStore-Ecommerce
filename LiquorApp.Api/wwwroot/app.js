const form = document.getElementById("form");
const submit = document.getElementById("submit");
const result = document.getElementById("result");
const fields = ["firstName","lastName","email","password","confirmPassword"]; 

const get = id => document.getElementById(id);
const errorEl = name => document.querySelector(`[data-error-for="${name}"]`);

function validate() {
  let valid = true;
  const values = Object.fromEntries(fields.map(n => [n, get(n).value.trim()]));
  // clear errors
  fields.forEach(n => errorEl(n).textContent = "");

  if (!values.firstName) { errorEl('firstName').textContent = 'Nombre requerido'; valid = false; }
  if (!values.lastName) { errorEl('lastName').textContent = 'Apellido requerido'; valid = false; }
  if (!values.email) { errorEl('email').textContent = 'Email requerido'; valid = false; }
  else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(values.email)) { errorEl('email').textContent = 'Email inválido'; valid = false; }

  if (!values.password) { errorEl('password').textContent = 'Contraseña requerida'; valid = false; }
  else {
    if (values.password.length < 8) { errorEl('password').textContent = 'Mínimo 8 caracteres'; valid = false; }
    if (!/[A-Z]/.test(values.password)) { errorEl('password').textContent = 'Debe incluir mayúscula'; valid = false; }
    if (!/[a-z]/.test(values.password)) { errorEl('password').textContent = 'Debe incluir minúscula'; valid = false; }
    if (!/[0-9]/.test(values.password)) { errorEl('password').textContent = 'Debe incluir número'; valid = false; }
    if (!/[^A-Za-z0-9]/.test(values.password)) { errorEl('password').textContent = 'Debe incluir símbolo'; valid = false; }
  }

  if (!values.confirmPassword) { errorEl('confirmPassword').textContent = 'Confirmación requerida'; valid = false; }
  else if (values.password !== values.confirmPassword) { errorEl('confirmPassword').textContent = 'No coincide'; valid = false; }

  submit.disabled = !valid;
  return { valid, values };
}

form.addEventListener('input', validate);
form.addEventListener('submit', async (e) => {
  e.preventDefault();
  const { valid, values } = validate();
  if (!valid) return;

  submit.disabled = true;
  result.textContent = '';

  try {
    const res = await fetch('/api/users/register', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(values)
    });
    if (res.ok) {
      const data = await res.json();
      result.textContent = `Usuario creado: ${data.email}`;
      result.className = 'ok';
      form.reset();
    } else if (res.status === 409) {
      const { message } = await res.json();
      result.textContent = message || 'Email ya está en uso';
      result.className = '';
    } else if (res.status === 400) {
      const problem = await res.json();
      if (problem && problem.errors) {
        for (const [key, msgs] of Object.entries(problem.errors)) {
          errorEl(key)?.textContent = Array.isArray(msgs) ? msgs.join(', ') : String(msgs);
        }
      }
      result.textContent = 'Revisa los datos ingresados.';
      result.className = '';
    } else {
      result.textContent = 'Error al registrar. Intente nuevamente.';
      result.className = '';
    }
  } catch (err) {
    result.textContent = 'Error de red: ' + err;
  } finally {
    submit.disabled = false;
  }
});

validate();