window.Download = (texto, nome) => {
    const a = document.createElement('a');
    const blob = new Blob([texto], { type: 'text/plain' });
    const url = URL.createObjectURL(blob);
    a.setAttribute('href', url);
    a.setAttribute('download', nome + ".txt");
    a.click();
}