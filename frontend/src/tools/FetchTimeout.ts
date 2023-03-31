export function fetchWithTimeout(fetch: Promise<Response>, timeout: number) {
  const fetchPromise = fetch;
  const aboutPromise = new Promise((resolve, reject) => {
    setTimeout(() => {
      reject('Fetch request timed out');
    }, timeout);
  });

  const combinedPromise = Promise.race([fetchPromise, aboutPromise]);
  return combinedPromise;
}

export function fetchJSONWithTimeout(
  fetch: Promise<Response>,
  timeout: number
) {
  return fetchWithTimeout(fetch, timeout).then((response) => {
    if (response instanceof Response) {
      if (response.status === 200) {
        return response.json();
      }
    }
  });
}
